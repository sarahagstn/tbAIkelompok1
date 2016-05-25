<?php

class home_m extends CI_Model {

private $primary_key='id_air';
private $table_name='air';

function __construct () {
	parent ::__construct ();
	}
	function get_paged_list ($limit=10,$offset=0,
	$order_column='',$order_type='asc')
	{
	if (empty ($order_column) || empty ($order_type))
	$this->db->order_by($this->primary_key,'asc');
	else
	$this->db->order_by($order_column,$order_type);
	return $this->db->get($this->table_name,$limit,$offset);
	}
	function index()
	{
	$query = $this->db->select('id_air,status,tanggal')
        ->from('air')
        ->get();
    return $query->result() ;
    }
	function get_by_id($id){
	$this->db->where($this->primary_key,$id);
	return $this->db->get($this->table_name);
	}
	function jumlah_data(){
	return $this->db->get($this->table_name);
	
	}
}